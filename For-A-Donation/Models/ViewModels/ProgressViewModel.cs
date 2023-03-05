using For_A_Donation.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels;

public class ProgressViewModel
{
    public CategoryOfTask CategoryOfTask { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Value is out of range")]
    public int PointsEnd { get; set; }
}
