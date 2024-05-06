using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations
{
    class StringIslemleri
    {
        static void Main()
        {
            String isim = "Esenyurt Üniversitesi";
            isim.ToUpper(); //Büyük Harf Yapar
            isim.ToLower(); //Küçük Harf Yapar
            isim.Trim();//Boşluk karakterini atar
            isim.Remove(5);//5. indexten sonrası siler
            isim.Substring(4);//4. indexten sonrasını getirir
            isim.Substring(5, 10);//5. indexten sonra 10 karakter getir

            
        }
    }
}
