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
        public void resolveOffers()
        {
            foreach (Book book in this.bookList)
            {
                Console.WriteLine("\nResult of '" + Commodities.getOneById(book.commodityId).getName() + "' commodity");
                book.printOffers();
            }
        }




    }
}