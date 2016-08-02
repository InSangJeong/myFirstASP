﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebApplication2.dataSet
{
    public class Seat
    {
        public string TheaterID { get; set; }
        public string Seatrow { get; set; }
        public string Seatnumber { get; set; }
        public string Isbooked{ get; set; }
        public string Playtime { get; set; }


        static public List<Seat> SqlDataReaderToSeat(SqlDataReader Reader)
        {
            List<Seat> Seats = new List<Seat>();
            try
            {
                while (Reader.Read())
                {
                    Seat seat = new Seat();
                    seat.TheaterID = Reader["TheaterID"].ToString().Trim();
                    seat.Seatrow = Reader["Seatrow"].ToString().Trim();
                    seat.Seatnumber = Reader["Seatnumber"].ToString().Trim();
                    seat.Isbooked = Reader["Isbooked"].ToString().Trim();
 
                    seat.Playtime = Reader["Playtime"].ToString();

                    Seats.Add(seat);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return Seats;
        }
    }
}