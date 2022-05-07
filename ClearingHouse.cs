namespace econrpg
{
    public class ClearingHouse
    {
        public List<Book> bookList; 
        public ClearingHouse() {
            this.bookList = new List<Book>();
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
        private void resolveOneExchange(Offer askOffer, Offer bidOffer)
        {
            Console.WriteLine(askOffer);
            Console.WriteLine(bidOffer);
            double meanPrice = (askOffer.price + bidOffer.price) / 2;
            int lowerAmount = Math.Min(askOffer.getUnfilledAmount(), bidOffer.getUnfilledAmount());
            askOffer.trade(lowerAmount, meanPrice);
            bidOffer.trade(lowerAmount, meanPrice);

            Console.WriteLine("lowerAmount " + lowerAmount);
            Console.WriteLine("meanPrice " + meanPrice);
            
            Console.WriteLine("\nEnd of one Exchange");
            Console.WriteLine("\nask results: ");
            Console.WriteLine(askOffer.filledAmount);
            Console.WriteLine(askOffer.wallet);
            Console.WriteLine("\nbid results: ");
            Console.WriteLine(bidOffer.filledAmount);
            Console.WriteLine(bidOffer.wallet);
            
        }
        public void resolveOffers()
        {
            foreach (int commodityId in getListOfUniqueCommodityId())
            {
                Console.WriteLine("\nResult of '" + Commodities.getOneById(commodityId).getName() + "' commodity");
                List<Book> books = this.getBooksPair(commodityId); 

                if (books.Count != 2) continue;

                books.ForEach(x => x.sortOffers());
                books.ForEach(x => x.printOffers());
                   
                Book limitingBook = this.getBookWithLessAmount(books);
                
                while (limitingBook.stillUnfilledOffers())
                {
                    Offer askOffer = books[0].getOpenOfferOnTop();
                    Offer bidOffer = books[1].getOpenOfferOnTop();
                    Console.WriteLine(askOffer);
                    Console.WriteLine(bidOffer);
                    this.resolveOneExchange(askOffer, bidOffer);
                }
            }
        }




    }
}