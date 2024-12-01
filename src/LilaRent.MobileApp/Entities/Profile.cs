using LilaRent.Domain.Fields;


namespace LilaRent.MobileApp.Entities;


public class Profile
{
	public long Id { get; set; }
	public string Name { get; set; }

	public string ImagePath { get; set; } = "sigmastare1.jpg";

	public string Caption { get; set; } = "If you believe the westerrn sun Is falling down on everyone And you feel it burn, dont't try to run" +
										  "And you feel it burn, your time has come";

	public float Rate { get; set; } = 3.5f;

	public LegalEntityType LegalEntityType { get; init; } = LegalEntityType.LegalPerson;

}
