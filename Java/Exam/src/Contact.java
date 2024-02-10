import java.util.Random;

public class Contact {
    public int id = new Random().nextInt(1, 1000000);
    public String FullName;
    public String Address;
    public String Phone;
    public Category ContactCategory;
    public String Description;

    public Contact(String fullName, String address, String phone, Category category, String description) {
        FullName = fullName;
        Address = address;
        Phone = phone;
        ContactCategory = category;
        Description = description;
    }

    public Contact() {}
}
