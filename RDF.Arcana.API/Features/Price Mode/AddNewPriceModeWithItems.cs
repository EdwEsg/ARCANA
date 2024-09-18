//using RDF.Arcana.API.Common;
//using RDF.Arcana.API.Data;
//using RDF.Arcana.API.Domain;

//namespace RDF.Arcana.API.Features.Price_Mode
//{
//    public class AddNewPriceModeWithItems
//    {
//        public class AddNewPriceModeWithItemsCommand : IRequest<Result>
//        {
//            public string PriceModeCode { get; set; }
//            public string PriceModeDescription { get; set; }
//            public int AddedBy { get; set; }
//            public ICollection<Item> Items { get; set; }
//            public class Item
//            {
//                public int PriceModeId { get; set; }
//                public int ItemId { get; set; }
//                public decimal Price { get; set; }
//                public string Remarks { get; set; }
//            }

//        }

//        public class Handler : IRequestHandler<AddNewPriceModeWithItemsCommand, Result>
//        {
//            private readonly ArcanaDbContext _context;
//            public Handler(ArcanaDbContext context)
//            {
//                _context = context;
//            }

//            public Task<Result> Handle(AddNewPriceModeWithItemsCommand request, CancellationToken cancellationToken)
//            {
//                var priceModeWithItems = _context.PriceModeItems
//                    .Include(p => p.PriceMode)
//                    .Include(pmi => pmi.ItemPriceChanges)
//                    .Include(i => i.Item);

//                var priceMode = new PriceMode
//                {
//                    PriceModeCode = request.PriceModeCode,
//                    PriceModeDescription = request.PriceModeDescription,
//                    AddedBy = request.AddedBy
//                };

//                _context.Add(priceMode);
//                _context.SaveChanges();

//                foreach (var item in request.Items)
//                {
//                    var priceModeItems = new PriceModeItems
//                    {
//                        PriceModeId = priceMode.Id,
//                        ItemId = item.ItemId,                       
//                    };


//                }



//            }
//        }
//    }
//}
