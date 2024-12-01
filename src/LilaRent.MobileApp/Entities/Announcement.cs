using LilaRent.MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Entities;
	/// <summary>
	/// This class represents Basic information about announcement to be displayed in list.
	/// </summary>
public class AnnouncementBasics
{
	public long Id { get; set; }	

	public string MainImagePath { get; set; }

	public string Title { get; set; }

	public float Rate { get; set; }

	public Category Category { get; set; }

	public bool IsCrowned => Id % 4 == 3;

    public string Address { get; init; } = "г. Минск, ул Долгобродская 12";
    public decimal Price { get; set; }

	public string Description { get; set; }

	public bool IsShortTerm { get; set; } = true;

    public string[] Images { get; init; } =
    {

        "sigmastare1.jpg",
        "sigmastare2.jpg",
        "sigmastare3.jpg",
    };
}

public class AnnouncementInfo
{
	public long Id { get; init; }

	public string MainImagePath { get; init; }

	public string Title { get; init; }

	public float Rate { get; init; }

	public Category Category { get; init; }

	public decimal Price { get; init; }

    public bool IsCrowned => Id % 4 == 3;

    public bool IsShortTerm { get; init; } = true;

	public string Description { get; init; }

	public Profile Profile { get; init; }

    public string Address { get; init; } = "г. Минск, ул Долгобродская 12";

    public string[] Images { get; init; } =
	{
        "sigmastare1.jpg",
        "sigmastare2.jpg",
        "sigmastare3.jpg",
    };
}
