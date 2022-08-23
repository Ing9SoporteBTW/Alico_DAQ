using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedService.Functions
{
    public class ProcessData
    {
        public List<List<T>> GenerateGroups<T>(List<T> teams, int amount)
        {
            if (amount == 0)
                amount = 1;

            int teamCount = (int)teams.Count / amount;
            List<T> allteams = teams.ToList(); // copy to be able to remove items

            if (teamCount == 0)
                return new List<List<T>> { allteams };

            List<List<T>> allTeamGroups = new List<List<T>>();
            List<T> thisTeam = new List<T>();
            while (allteams.Count > 0)
            {
                if (thisTeam.Count == amount)
                {
                    allTeamGroups.Add(thisTeam);
                    thisTeam = new List<T>();
                }
                thisTeam.Add(GetAndRemoveRandomTeam(allteams));
            }
            allTeamGroups.Add(thisTeam);

            return allTeamGroups;
        }

        private T GetAndRemoveRandomTeam<T>(List<T> allTeams)
        {
            Random _rnd = new();
            int randomIndex = _rnd.Next(allTeams.Count);
            T randomTeam = allTeams[randomIndex];
            allTeams.RemoveAt(randomIndex);
            return randomTeam;
        }
    }
}
