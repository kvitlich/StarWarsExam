using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    public class PageHasher<T>
    {
        private static List<KeyValuePair<int, List<T>>> Pages = new List<KeyValuePair<int, List<T>>>();

        public bool HasThisPage(int index)
        {
            foreach (var page in Pages)
            {
                if (page.Key == index)
                    return true;
            }
            return false;
        }

        public void AddPageInfo(List<T> newPage, int index)
        {
            Pages.Add(new KeyValuePair<int, List<T>>(index, newPage));
        }     

        public List<T> GetPage(int index)
        {
            foreach (var page in Pages)
            {
                if (page.Key == index)
                    return page.Value;
            }
            return null;
        }

        public void ClearHash()
        {
            Pages.Clear();
        }
    }
}
