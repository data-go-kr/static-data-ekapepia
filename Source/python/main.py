import requests
import sys
import os
import datetime

for arg in sys.argv:
    print(arg)

URL = 'http://data.ekape.or.kr/openapi-data/service/user/grade/auct/beefGrade?serviceKey={serviceKey}&startYmd={startYmd}&endYmd={endYmd}&abattCd={abattCd}&sexCd={sexCd}'
URL = URL.format( serviceKey = sys.argv[1], startYmd = "20160120", endYmd = "20160120", abattCd = "0302", sexCd = 1)
print(URL)

DataPath = 'Data-python'
Now = datetime.datetime.now()

# 폴더 생성 작성
def CreateDirectory(path):
    if not os.path.exists(path):
        os.makedirs(path)

# 모든 텍스트 파일 저장 작성
def WriteAllText(path, contents):
    print(path)
    with open(path, "w") as file:
        file.write(contents)

response = requests.get(URL)
#response.status_code
#response.text

Body = response.text

CreateDirectory(DataPath)
WriteAllText(os.path.join(DataPath, "legacy-latest.xml"), Body)

DataPath = os.path.join(DataPath, Now.strftime("%Y%m%d"))

CreateDirectory(DataPath)
WriteAllText(os.path.join(DataPath, "{file}.xml".format( file = Now.strftime("%Y%m%d %H%M"))), Body)

#print(response.text)