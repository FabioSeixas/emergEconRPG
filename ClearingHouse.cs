namespace econrpg
{
    public class ClearingHouse
    {
        private List<Book> bookList; 
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
            return this.bookList.FindAll(x => x.commodityId == commodityId);
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
        public void resolveOffers()
        {
            foreach (int commodityId in getListOfUniqueCommodityId())
            {
                Console.WriteLine("\nResult of '" + Commodities.getOneById(commodityId).getName() + "' commodity");
                List<Book> books = this.getBooksPair(commodityId); 

                books.ForEach(x => x.sortOffers());
                   
                Book limitingBook = this.getBookWithLessAmount(books);
                
                // while (limitingBook.thereStillUnfilledOffers())

                foreach (Book book in books)
                {
                    book.printOffers();
                }
            }
        }




    }
}