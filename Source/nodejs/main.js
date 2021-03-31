var request = require('request');
var fs = require('fs');
var path = require('path');
var dateFormat = require("dateformat");
var datetime = new Date();

var url = 'http://data.ekape.or.kr/openapi-data/service/user/grade/auct/beefGrade';
var queryParams = '?';
queryParams += '&' + encodeURIComponent('startYmd') + '=' + encodeURIComponent('20160120'); /* startYmd */
queryParams += '&' + encodeURIComponent('endYmd') + '=' + encodeURIComponent('20160120'); /* endYmd */
queryParams += '&' + encodeURIComponent('abattCd') + '=' + encodeURIComponent('0302'); /* abattCd */
queryParams += '&' + encodeURIComponent('sexCd') + '=' + encodeURIComponent('1'); /* sexCd */
queryParams += '&' + encodeURIComponent('serviceKey') + '=' + process.argv[2]; /* Service Key*/

console.log(url + queryParams);

// 폴더 생성 작성
function CreateDirectory(path) { 
    if (!fs.existsSync(path)){
        fs.mkdirSync(path);
    }
}

// 모든 텍스트 파일 저장 작성
function WriteAllText(path, contents) { 
    fs.writeFile(path, contents, function(err) {
        if(err) {
            return console.log(err);
        }
    }); 
}

var DataPath = 'Data-nodejs'

request({
    url: url + queryParams,
    method: 'GET'
}, function (error, response, body) {

    CreateDirectory(DataPath);
    WriteAllText(path.join(DataPath, "legacy-latest.xml"), body);

    DataPath = path.join(DataPath, dateFormat(datetime, "yyyymmdd"));

    CreateDirectory(DataPath);
    WriteAllText(path.join(DataPath, dateFormat(datetime, "yyyymmdd HHMM") + ".xml"), body);
});