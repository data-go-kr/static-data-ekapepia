using System;
using System.Xml.Linq;

namespace Project
{
    class Program
    {
        static int Main(string[] args)
        {
            if(args.Length == 0)
            {
                return -1;
            }

            // 가지고 오기
            var _Body = new System.Net.WebClient()
            {
                Encoding = System.Text.Encoding.UTF8
            }.DownloadString($@"http://data.ekape.or.kr/openapi-data/service/user/grade/auct/beefGrade?serviceKey={args[0]}&startYmd=20160120&endYmd=20160120&abattCd=0302&sexCd=1");

            // 문법 정렬
            var _XDocument = XDocument.Parse(_Body);
            _Body = $"<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n{_XDocument.ToString()}";

            var _Now = System.DateTime.Now;

            var _CurrentPath = System.IO.Directory.GetCurrentDirectory();
            _CurrentPath = System.IO.Path.Combine(_CurrentPath, "Data-csharp");

            Console.WriteLine(_CurrentPath);

            // 폴더 생성 - current

            System.IO.Directory.CreateDirectory(_CurrentPath);

            // 파일 저장 - current
            System.IO.File.WriteAllText(System.IO.Path.Combine(_CurrentPath, "legacy-latest.xml"), _Body, System.Text.Encoding.UTF8);

            // 폴더 - 일자별
            _CurrentPath = System.IO.Path.Combine(_CurrentPath, _Now.ToString("yyyyMMdd"));

            // 폴더 생성 - 일자별
            System.IO.Directory.CreateDirectory(_CurrentPath);

            // 파일 저장 - current
            System.IO.File.WriteAllText(System.IO.Path.Combine(_CurrentPath, $"{_Now.ToString("yyyyMMdd HHmm")}.xml"), _Body, System.Text.Encoding.UTF8);

            return 0;
        }
    }
}
