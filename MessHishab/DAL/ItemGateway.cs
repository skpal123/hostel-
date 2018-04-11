using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using MessHishab.Models;

namespace MessHishab.DAL
{
    public class ItemGateway
    {
        string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString.ToString();
        //public List<DateCls> getMealChartView(DateTime date)
        //{
        //    List<DateCls> dateClsList = new List<DateCls>();
        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("", date);
        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        while(rdr.Read())
        //    }
        //}
        public void saveMealList(List<MealList> mealList)
        {
            StringBuilder sb = new StringBuilder();
            int id = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                for (int i = 0; i < mealList.Count; i++)
                {
                    sb.Append(" insert into MealChart (Id,MemberId,Morning,Midday,Evening,Date) values(@param1" + i + ",@param2" + i + ",@param3" + i + ",@param4" + i + ",@param5" + i + ",@param6" + i + ")");
                }
                SqlCommand cmd = new SqlCommand("delete from MealChart where month(Date)=month(getdate());"+sb.ToString(), con);
                for (int i = 0; i < mealList.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@param1" + i + "", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@param2" + i + "", mealList[i].MemberId);
                    cmd.Parameters.AddWithValue("@param3" + i + "", mealList[i].Morning);
                    cmd.Parameters.AddWithValue("@param4" + i + "", mealList[i].Lunch);
                    cmd.Parameters.AddWithValue("@param5" + i + "", mealList[i].Dinner);
                    cmd.Parameters.AddWithValue("@param6" + i + "", Convert.ToDateTime(mealList[i].Date));
                }
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<MealList> getAllMealList()
        {
            List<MealList> mealListList = new List<MealList>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from MealChart",con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    MealList mealList = new MealList();
                    mealList.MemberId = Guid.Parse(rdr["MemberId"].ToString());
                    mealList.Morning = Convert.ToDouble(rdr["Morning"]);
                    mealList.Lunch = Convert.ToDouble(rdr["Midday"]);
                    mealList.Dinner = Convert.ToDouble(rdr["Evening"]);
                    mealList.Date = rdr["Date"].ToString();
                    mealListList.Add(mealList);
                }
            }
            return mealListList;
        }
        public List<Category1> GetCategory()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString.ToString();
            List<Category1> items = new List<Category1>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from tblCategory", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Category1 item = new Category1();
                    item.Id = Guid.Parse(rdr[0].ToString());
                    item.Name = rdr[1].ToString();
                    items.Add(item);
                }
            }

            return items;
        }
        public List<ItemInfo> getItemList(Guid? CategoryId)
        {
            int serialNo = 1;
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString.ToString();
            List<ItemInfo> items = new List<ItemInfo>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select a.Id,a.Name as itemName,b.Name from tblItem a inner join tblUnit b on a.UnitId=b.Id where a.CategoryId=@CategoryId", con);
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ItemInfo item = new ItemInfo();
                    item.SerialNo = serialNo;
                    item.Id = Guid.Parse(rdr[0].ToString());
                    item.Name = rdr[1].ToString();
                    item.UnitName = rdr["Name"].ToString();
                    items.Add(item);
                    serialNo++;
                }
            }
            return items;
        }
        public void saveBajarList(List<BajarList> bajarList)
        {
            StringBuilder sb = new StringBuilder();
            int id = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                for (int i = 0; i < bajarList.Count; i++)
                {
                    sb.Append("insert into DailyBazarList (Id,Date,Quantity,Price,PurchaserId,ItemId) values(@param1" + i + ",@param2" + i + ",@param3" + i + ",@param4" + i + ",@param5" + i + ",@param6" + i + ")");
                }
                SqlCommand cmd = new SqlCommand(sb.ToString(), con);
                for (int i = 0; i < bajarList.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@param1" + i + "", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@param2" + i + "", bajarList[i].Date);
                    cmd.Parameters.AddWithValue("@param3" + i + "", bajarList[i].Quantity);
                    cmd.Parameters.AddWithValue("@param4" + i + "", bajarList[i].Price);
                    cmd.Parameters.AddWithValue("@param5" + i + "", bajarList[i].PurchaserId);
                    cmd.Parameters.AddWithValue("@param6" + i + "", bajarList[i].ItemId);
                }
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<Member> getAllMember()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString.ToString();
            List<Member> members = new List<Member>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from tblMember", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Member member = new Member();
                    member.Id = Guid.Parse(rdr[0].ToString());
                    member.Name = rdr[1].ToString();
                    members.Add(member);
                }
            }
            return members;
        }
        public List<BajarList> getBajarListBymemberId(string Date,Guid memberId)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString.ToString();
            List<BajarList> bjarLists = new List<BajarList>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from DailyBazarList where PurchaserId=@PurchaserId and Date=@date", con);
                cmd.Parameters.AddWithValue("@PurchaserId", memberId);
                cmd.Parameters.AddWithValue("@date", Date);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    BajarList bajarList = new BajarList();
                    bajarList.Id = Guid.Parse(rdr["Id"].ToString());
                    bajarList.ItemId = Guid.Parse(rdr["ItemId"].ToString());
                    bajarList.Date = rdr["ItemId"].ToString();
                    bajarList.Quantity = Convert.ToDouble(rdr["Quantity"].ToString());
                    bajarList.PurchaserId = Guid.Parse(rdr["PurchaserId"].ToString());
                    bajarList.Price = Convert.ToDouble(rdr["Price"].ToString());
                    bjarLists.Add(bajarList);
                }
            }

            return bjarLists;
        }

    }
}