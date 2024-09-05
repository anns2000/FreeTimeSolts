using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<List<DateTime>> usersUnavailableTimes = new List<List<DateTime>>
        {
            new List<DateTime>
            {
                DateTime.Today.AddHours(11).AddMinutes(0),
                DateTime.Today.AddHours(11).AddMinutes(30),
                DateTime.Today.AddHours(12).AddMinutes(0),
                DateTime.Today.AddHours(12).AddMinutes(30),
                DateTime.Today.AddHours(13).AddMinutes(0),
                DateTime.Today.AddHours(13).AddMinutes(30)
            },
            new List<DateTime>
            {
                DateTime.Today.AddHours(11).AddMinutes(0),
                DateTime.Today.AddHours(11).AddMinutes(30),
            },
            new List<DateTime>
            {
                DateTime.Today.AddHours(14).AddMinutes(0),
                DateTime.Today.AddHours(14).AddMinutes(30),
            }
        };

        int newMeetingHours = 1;
        int windowSize = newMeetingHours * 6;  // Number of 10-minute slots needed

        // Create all possible 10-minute slots from 09:00 to 16:00
        var timeSlots = new List<DateTime>();
        for (int h = 9; h < 16; h++)
        {
            for (int m = 0; m < 60; m += 10)
            {
                timeSlots.Add(DateTime.Today.AddHours(h).AddMinutes(m));
            }
        }

        int i = 0;
        int j = 0;
        int validCount = 0;
        List<string> freeTimeSlots = new List<string>();

        while (j < timeSlots.Count)
        {
            if (j - i + 1 <= windowSize)
            {
                if (IsFreeSlot(timeSlots[j], usersUnavailableTimes))
                {
                    validCount++;
                }
                j++;

                if (j - i == windowSize && validCount == windowSize)
                {
                    string availableSlot = $"Available Slot: {timeSlots[i]:HH:mm} - {timeSlots[j - 1].AddMinutes(10):HH:mm}";
                    freeTimeSlots.Add(availableSlot);
                }
            }
            else
            {
                if (IsFreeSlot(timeSlots[i], usersUnavailableTimes))
                {
                    validCount--;
                }
                i++;
            }
        }

        if (freeTimeSlots.Count > 0)
        {
            foreach (var time in freeTimeSlots)
            {
                Console.WriteLine(time);
            }
        }
        else
        {
            Console.WriteLine("We can't find a meeting slot.");
        }
    }

    static bool IsFreeSlot(DateTime time, List<List<DateTime>> usersUnavailableTimes)
    {
        foreach (var userTimes in usersUnavailableTimes)
        {
            if (userTimes.BinarySearch(time) >= 0)
            {
                return false;
            }
        }
        return true;
    }
}
