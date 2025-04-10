using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.Inventory;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Inventory_Management
{
    [Route("api/add-misc-in"), ApiController]
    public class AddMiscIn : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddMiscIn(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddMiscInCommand command)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.CreatedBy = userId;
                }
                var result = await _mediator.Send(command);
                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class AddMiscInCommand : IRequest<Result>
        {
            public int CreatedBy { get; set; }
            public List<MoveOrderItemDtoForMiscIn> Items { get; set; }
            public class MoveOrderItemDtoForMiscIn
            {
                public string ItemCode { get; set; }
                public decimal? ActualQuantity { get; set; }
                public string ProductionDate { get; set; }
                public string Reason { get; set; }
            }
        }

        public class Handler : IRequestHandler<AddMiscInCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddMiscInCommand request, CancellationToken cancellationToken)
            {
                var itemCodes = request.Items.Select(i => i.ItemCode).Distinct().ToList();
                var itemsInContext = await _context.Items
                    .Where(i => itemCodes.Contains(i.ItemCode))
                    .ToListAsync(cancellationToken);

                var miscMoveOrder = new MoveOrder
                {
                    CreatedById = request.CreatedBy,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Type = Status.MiscIn
                };

                await _context.MoveOrders.AddAsync(miscMoveOrder, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var miscMoveOrderItemsList = new List<MoveOrderItem>();
                foreach (var item in request.Items)
                {
                    var itemCode = itemsInContext.First(i => i.ItemCode == item.ItemCode);

                    var miscMoveOrderItems = new MoveOrderItem
                    {
                        MoveOrderId = miscMoveOrder.Id,
                        ItemCode = item.ItemCode,
                        Quantity = item.ActualQuantity ?? 0,
                        ActualQuantity = item.ActualQuantity ?? 0,
                        ProductionDate = item.ProductionDate,
                        ItemId = itemCode.Id,
                        UomId = itemCode.UomId,
                        IsActive = true,
                        CreatedBy = _context.Users.FirstOrDefault(u => u.Id == request.CreatedBy),
                        Reason = item.Reason,
                        RemainingQuantity = item.ActualQuantity ?? 0
                    };

                    miscMoveOrderItemsList.Add(miscMoveOrderItems);
                }

                await _context.MoveOrderItems.AddRangeAsync(miscMoveOrderItemsList, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
