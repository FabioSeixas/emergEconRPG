namespace econrpg
{
    public class ClearingHouse
    {
        private int round;
        public List<Book> bookList; 
        public ClearingHouse() {
            this.round = 0;
            this.bookList = new List<Book>();
        }
        public int Round
        {
            get { return this.round; }
            set { this.round++; }
        }
        private double getTwoBooksAmountRatio(int askTotalAmount, int bidTotalAmount)
        {
            if (bidTotalAmount <= 0) return 0.0;
            double askBidAmountRatio = Convert.ToDouble(askTotalAmount) / Convert.ToDouble(bidTotalAmount);
            return Math.Round(askBidAmountRatio, 2);
        }
        private Book newBook(String type, int commodityId)
        {
            Book newBook = new Book(type, commodityId);
            this.bookList.Add(newBook);
            return newBook;
        }
        private IEnumerable<int> getListOfUniqueCommodityId()
        {
            return this.bookList.Select(x => x.commodityId).Distinct();
        }
        private List<Book> getBooksPair(int commodityId)
        {
            List<Book> books = this.bookList.FindAll(x => x.commodityId == commodityId);
            books.Sort(delegate(Book x, Book y)
            {
                if (x.type == null && y.type == null) return 0;
                else if (x.type == null) return -1;
                else if (y.type == null) return 1;
                else return x.type.CompareTo(y.type);
            });
            // "ask" book will always be the index 0;
            return books;
        }
        public void receiveOffers(List<Offer> offers)
        {
            foreach(Offer offer in offers)
            {
                Book book = this.bookList.Find(x => (x.commodityId == offer.commodityId & x.type == offer.type));
                if (book != null) {
                    book.addOffer(offer);
                    continue;
                }
                Book newBook = this.newBook(offer.type, offer.commodityId);
                newBook.addOffer(offer);
            }
        }
        private Book getBookWithLessAmount(List<Book> books)
        {
            int first = books[0].getOffersTotalAmount();
            int second = books[1].getOffersTotalAmount();
            return first > second ? books[1] : books[0];
        }
        private Exchange resolveOneExchange(Offer askOffer, Offer bidOffer)
        {
            double meanPrice = (askOffer.price + bidOffer.price) / 2;
            int lowerAmount = Math.Min(askOffer.getUnfilledAmount(), bidOffer.getUnfilledAmount());
            askOffer.trade(lowerAmount, meanPrice);
            bidOffer.trade(lowerAmount, meanPrice);

            StorageStatic.writeLine("trade", new TradeStats { 
                AskOfferPrice = askOffer.price,
                BidOfferPrice = bidOffer.price,
                CommodityId = askOffer.commodityId,
                DealPrice = meanPrice,
                Round = this.Round,
                TradeAmount = lowerAmount
            });

            return new Exchange { DealAmount = lowerAmount, DealPrice = meanPrice };
        }
        public void resolveOffers()
        {
            IEnumerable<int> list = this.getListOfUniqueCommodityId();
            foreach (int commodityId in this.getListOfUniqueCommodityId())
            {
                Exchanges exchangesList = new Exchanges();
                List<Book> books = this.getBooksPair(commodityId); 

                if (books.Count != 2) continue;

                books.ForEach(x => x.sortOffers());
                   
                Book limitingBook = this.getBookWithLessAmount(books);
                
                while (limitingBook.stillOpenOffers())
                {
                    Offer askOffer = books[0].getOpenOfferOnTop();
                    Offer bidOffer = books[1].getOpenOfferOnTop();
                    Exchange exchangeResult = this.resolveOneExchange(askOffer, bidOffer);
                    exchangesList.addExchange(exchangeResult);
                }

                // books.ForEach(x => x.printOffers());

                int askTotalAmount = books[0].getOffersTotalAmount();
                int bidTotalAmount = books[1].getOffersTotalAmount();

                StorageStatic.writeLine("commodity", new CommodityStats {
                    Round = this.Round,
                    CommodityId = commodityId,
                    askPriceAvg = books[0].getOffersPriceAvg(),
                    AskTotalAmount = askTotalAmount,
                    bidPriceAvg = books[1].getOffersPriceAvg(),
                    BidTotalAmount = bidTotalAmount,
                    SDRatio = this.getTwoBooksAmountRatio(askTotalAmount, bidTotalAmount),
                    dealPriceAvg = exchangesList.getAvgDealPrice(),
                    totalAmountTraded = exchangesList.getTotalDealAmount()
                });
            }

            this.bookList.ForEach(x => x.finishOffers());
        }
    }

    class Exchange
    {
        public double DealPrice { get; set; }
        public int DealAmount { get; set; }
    }

    class Exchanges
    {
        private List<Exchange> exchanges = new List<Exchange>();
        public double getAvgDealPrice()
        {
            if (this.exchanges.Count() <= 0) return 0.0;
            IEnumerable<double> offersPrices = this.exchanges.Select(offer => offer.DealPrice);
            double sum = offersPrices.Aggregate(0.0, (total, price) => total + price);
            return Math.Round(sum / offersPrices.Count(), 2); 
        }
        public int getTotalDealAmount()
        {
            return this.exchanges.Aggregate(0, (total, current) => total + current.DealAmount);
        }
        public void addExchange(Exchange exchange)
        {
            this.exchanges.Add(exchange);
        }
    }
}
