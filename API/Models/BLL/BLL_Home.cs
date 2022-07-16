namespace API.Models.BLL;

public class BllHome
{
    public object GetInfos(string isbn)
    {
        BllAbeBooks bllAbeBooks = new BllAbeBooks();
        BllAmazon bllAmazon = new BllAmazon();
        BllBookFinder bllBookFinder = new BllBookFinder();
        BllCpu bllCpu = new BllCpu();
        BllAlManhal bllAlManhal = new BllAlManhal();

        var result = bllBookFinder.GetInfoFromBookFinder(isbn);
        if (result.GetType().GetProperty("error") != null)
        {
            result = bllAbeBooks.GetInfoFromAbeBooks(isbn);
            if (result.GetType().GetProperty("error") != null)
            {
                result = bllAmazon.GetInfoFromAmazon(isbn);
                if (result.GetType().GetProperty("error") != null)
                {
                    result = bllAlManhal.GetInfoFromALManhal(isbn);
                    if (result.GetType().GetProperty("error") != null)
                    {
                        result = bllCpu.GetInfoFromCpu(isbn);
                    }
                }
            }
        }

        return result;
    }
}