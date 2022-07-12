namespace API.BLL;

public class BLL_Home
{
    public object GetInfos(string isbn)
    {
        object result;
        BLL_AbeBooks bll_AbeBooks = new BLL_AbeBooks();
        BLL_Amazon bll_Amazon = new BLL_Amazon();
        BLL_BookFinder bll_BookFinder = new BLL_BookFinder();
        BLL_Cpu bll_Cpu = new BLL_Cpu();
        BLL_ALManhal bll_ALManhal = new BLL_ALManhal();

        result = bll_BookFinder.GetInfoFromBookFinder(isbn);
        if (result.GetType().GetProperty("error") != null)
        {
            result = bll_Amazon.GetInfoFromAmazon(isbn);
            if (result.GetType().GetProperty("error") != null)
            {
                result = bll_AbeBooks.GetInfoFromAbeBooks(isbn);
                if (result.GetType().GetProperty("error") != null)
                {
                    result = bll_Cpu.GetInfoFromCpu(isbn);
                    if (result.GetType().GetProperty("error") != null)
                    {
                        result = bll_ALManhal.GetInfoFromALManhal(isbn);
                    }
                }
            }
        }
        return result;
        
    }
}