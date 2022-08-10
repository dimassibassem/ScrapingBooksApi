namespace API.Models.BLL;

public static class BllHome
{
    public static object GetInfos(string isbn)
    {
        var result = BllAbeBooks.GetInfoFromAbeBooks(isbn);
        if (result.GetType().GetProperty("error") != null)
        {
            result = BllAbeBooks.GetInfoFromAbeBooks(isbn);
            if (result.GetType().GetProperty("error") != null)
            {
                result = BllAmazon.GetInfoFromAmazon(isbn);
                if (result.GetType().GetProperty("error") != null)
                {
                    result = BllAlManhal.GetInfoFromAlManhal(isbn);
                    if (result.GetType().GetProperty("error") != null)
                    {
                        result = BllCpu.GetInfoFromCpu(isbn);
                    }
                }
            }
        }

        return result;
    }
}