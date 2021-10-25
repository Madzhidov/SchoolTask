using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        public Dictionary<string, int> countWord(string FileContents)
        {

            const string UnwantedSigns = "<>.,?!-/*”“'\"";
            foreach (char c in UnwantedSigns)
            {
                FileContents = FileContents.Replace(c, ' ');
                GC.Collect();
            }
            FileContents = FileContents.Replace(Environment.NewLine, " ");
            string[] Words = FileContents.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            FileContents = string.Empty;
            GC.Collect();
            var WordCounts = new System.Collections.Concurrent.ConcurrentDictionary<string, int>();
            Parallel.ForEach(Words, line =>
            {
                WordCounts.AddOrUpdate(line, 1, (_, x) => x + 1);
            });
            Words = new string[1];
            var WordCounts2 = WordCounts.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            GC.Collect();

            return WordCounts2;
        }
    }
}
