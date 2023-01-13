using DebtorSearch.Common;
using MySql.Data.MySqlClient;
using NICS.System.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DebtorSearch.DatabaseReader
{
    public static class MySqlClass
    {
        public static DataTable GetConsultantActivityReport()
        {
            string query = @"SELECT ConsultantUserName AS 'Consultant',COUNT(DISTINCT AccountNumber) AS 'Accounts Worked',ptp AS 'Total PTP Captured',Amount AS 'Total PTP Amount',IFNULL(PhoneCalls,0) AS 'Total Phone Calls'
          FROM (SELECT
    'PTP' AS 'WorkType',
    COUNT(*) AS 'ptp',
    CAST('' AS CHAR(1)) AS 'Description',
    '' AS 'Status',
    '' AS 'ITAction',
    ROUND(SUM(PTP_AMOUNT), 2) 'Amount',
    TRIM(
      DATE_FORMAT(
        PTP_DATE_CAPTURE,
        '%Y-%m-%d %H:%i:%s'
      )
    ) 'CaptureDate',
    TRIM(SUBSTRING(PTP_DATE_PAY, 1, 10)) 'PayDate',
    TRIM(PTP_USER) 'ConsultantUserName',
    TRIM(GID_EMPLOYEE_NUMBER) 'ConsultantEmployeeNumber',
    PTP_ID 'UniqueID',
    TRIM(ACC_NUM) 'AccountNumber',
    TRIM(
      (SELECT
        CS_BOOK_NAME
      FROM
        coresetup
      LIMIT 1)
    ) 'ClientName',
    TRIM(
      DATE_FORMAT(PTP_DATE_CAPTURE, '%Y%m%d')
    ) 'DateID'      
  FROM
    ptp 
    LEFT JOIN gids 
      ON PTP_USER = GID_USER
  WHERE PTP_DATE_CAPTURE >= DATE(NOW())
    AND SUBSTRING(PTP_DATE_PAY, 1, 10) NOT LIKE '%0000%'
    AND PTP_DATE_CAPTURE NOT LIKE '%0000%' AND  PTP_USER NOT IN('IT','ADMIN')
  GROUP BY PTP_USER
  UNION
  ALL
  SELECT
    'NOTE' AS 'WorkType',
    0 AS 'ptp',
    TRIM(NOT_NOTE) AS 'Description',
    '' AS 'Status',
    '' AS 'ITAction',
    0 AS 'Amount',
    TRIM(NOT_DATE) 'CaptureDate',
    CAST('' AS CHAR(1)) AS 'PayDate',
    TRIM(NOT_USER) 'ConsultantUserName',
    TRIM(GID_EMPLOYEE_NUMBER) 'ConsultantEmployeeNumber',
    NOT_ID 'UniqueID',
    TRIM(ACC_NUM) 'AccountNumber',
    TRIM(
      (SELECT
        CS_BOOK_NAME
      FROM
        coresetup
      LIMIT 1)
    ) 'ClientName',
    TRIM(DATE_FORMAT(NOT_DATE, '%Y%m%d')) 'DateID'
  FROM
    notes 
    LEFT JOIN gids 
      ON NOT_USER= GID_USER
  WHERE NOT_DELETE = 0
    AND NOT_NOTE != ''
    AND NOT_DATE NOT LIKE '%0000%'
    AND NOT_DATE >= DATE(NOW()) AND NOT_USER NOT IN('IT','Admin')
    UNION
    ALL
    SELECT
      'Action' AS 'WorkType',
      0 AS 'ptp',
      TRIM(REP_ACTION) AS 'Description',
      TRIM(rep_status) 'Status',
      TRIM(REP_PROGRAM) 'ITAction',
      0 AS 'Amount',
      TRIM(CAST(REP_DATE AS CHAR(20))) AS 'CaptureDate',
      CAST('' AS CHAR(1)) AS 'PayDate',
      TRIM(REP_USID) 'ConsultantUserName',
      TRIM(GID_EMPLOYEE_NUMBER) 'ConsultantEmployeeNumber',
      REP_ID 'UniqueID',
      TRIM(ACC_NUM) 'AccountNumber',
      TRIM(
        (SELECT
          CS_BOOK_NAME
        FROM
          coresetup
        LIMIT 1)
      ) 'ClientName',
      TRIM(DATE_FORMAT(REP_DATE, '%Y%m%d')) 'DateID'
     FROM  report 
      LEFT JOIN gids 
        ON REP_USID = GID_USER
    WHERE REP_ACTION != ''
      AND REP_DATE NOT LIKE '%0000%'
      AND REP_DATE >= DATE(NOW()) AND REP_USID NOT IN('IT','Admin')
    GROUP BY REP_USID) t
    LEFT JOIN (SELECT COUNT(*) AS 'PhoneCalls',USER FROM filehistory 
    LEFT JOIN gids 
      ON USER = GID_USER
	WHERE ACTION = 'DIAL OUT'
               AND DATE >= CURDATE() AND USER NOT IN('IT','Admin')
               GROUP BY USER) TT
               ON T.ConsultantUserName=TT.User
    GROUP BY ConsultantEmployeeNumber
    ORDER BY Amount DESC";
            
            return MYSQL.Text(query).Query();


        }

        public static DataTable GetConsultantTotalNotesCaptured()
        {
            var query = @"SELECT NOT_USER AS 'Consultant', COUNT(*) AS 'Total Notes' FROM notes WHERE NOT_DATE >= DATE(NOW()) GROUP BY NOT_USER";
            return MYSQL.Text(query).Query();

        }

        public static List<SelectListItem> GetTableNames() {

            List<SelectListItem> items = new List<SelectListItem>();
       

        var query = @"SELECT schema_name AS 'Text', schema_name AS 'Value' FROM schemata ORDER BY schema_name ASC";


            var dt =  MYSQL.Text(query).Query();
            var studentList = (from DataRow dr in dt.Rows
                               select new SelectListItem()
                           {
                               Text= dr["Text"].ToString(),
                              Value = dr["Value"].ToString(),
                              
                           }).ToList();

            return studentList;


        }

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }


        public static DataTable ClientName() {

            var query = @"SELECT DATABASE() AS ClientName";
            return MYSQL.Text(query).Query();
        }



    }
}