using Microsoft.AspNetCore.Mvc.RazorPages;
using RomanianBurgerWeb.Data;
using System.Collections.Generic;
using System.Linq;

namespace RBweb.Models
{
    public class MeniuCategoriiPageModel : PageModel
    {
        public List<AssignedCategorieData> AssignedCategorieDataList;

        public void PopulateAssignedCategorieData(RomanianBurgerWebContext context,
                                                  Meniu meniu)
        {
            var allCategorii = context.Categorie;
            var meniuCategorii = new HashSet<int>(
                meniu.MeniuCategorii.Select(c => c.CategorieID));

            AssignedCategorieDataList = new List<AssignedCategorieData>();

            foreach (var cat in allCategorii)
            {
                AssignedCategorieDataList.Add(new AssignedCategorieData
                {
                    CategorieID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = meniuCategorii.Contains(cat.ID)
                });
            }
        }

        public void UpdateMeniuCategorii(RomanianBurgerWebContext context,
                                         string[] selectedCategorii,
                                         Meniu meniuToUpdate)
        {
            if (selectedCategorii == null)
            {
                meniuToUpdate.MeniuCategorii = new List<MeniuCategorie>();
                return;
            }

            var selectedHS = new HashSet<string>(selectedCategorii);
            var meniuCategorii = new HashSet<int>(
                meniuToUpdate.MeniuCategorii.Select(c => c.CategorieID));

            foreach (var cat in context.Categorie)
            {
                if (selectedHS.Contains(cat.ID.ToString()))
                {
                    if (!meniuCategorii.Contains(cat.ID))
                    {
                        meniuToUpdate.MeniuCategorii.Add(
                            new MeniuCategorie
                            {
                                MeniuID = meniuToUpdate.ID,
                                CategorieID = cat.ID
                            });
                    }
                }
                else
                {
                    if (meniuCategorii.Contains(cat.ID))
                    {
                        var toRemove = meniuToUpdate
                            .MeniuCategorii
                            .SingleOrDefault(i => i.CategorieID == cat.ID);

                        context.Remove(toRemove);
                    }
                }
            }
        }
    }
}
