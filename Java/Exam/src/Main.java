import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.File;
import java.io.IOException;
import java.util.*;

public class Main {

    public static void DisplayMenu() {
        System.out.println("1 - Show contacts");
        System.out.println("2 - Add contact");
        System.out.println("3 - Edit contact");
        System.out.println("4 - Delete contact");
        System.out.println("5 - Search contact");
        System.out.println("6 - Save");
        System.out.println("7 - Load");
        System.out.println("8 - Exit");
    }

    public static void main(String[] args) throws IOException {

        Scanner sc = new Scanner(System.in);
        PhoneBook book = new PhoneBook();
        ObjectMapper mapper = new ObjectMapper();

        while (true) {
            DisplayMenu();
            switch (sc.nextInt()) {
                case 1:
                    book.ShowContacts();
                    break;
                case 2:
                    Contact contact = new Contact();
                    System.out.print("FullName: ");
                    contact.FullName = sc.next();
                    System.out.print("Address: ");
                    contact.Address = sc.next();
                    System.out.print("Phone: ");
                    contact.Phone = sc.next();
                    System.out.print("Category: ");
                    contact.ContactCategory = Category.valueOf(sc.next());
                    System.out.print("Description: ");
                    contact.Description = sc.next();
                    book.AddContact(contact);
                    break;
                case 3:
                    break;
                case 4:
                    System.out.print("Id: ");
                    book.DeleteContact(sc.nextInt());
                    break;
                case 5:
                    System.out.print("Enter text for searching: ");
                    Contact[] founded = book.Search(sc.next());
                    for (Contact contact1 : founded)
                        System.out.printf("%s %s %n", contact1.FullName, contact1.Phone);
                    break;

                case 6:
                    mapper.writeValue(new File("data.json"), book);
                    break;
                case 7:
                    book = mapper.readValue(new File("data.json"), PhoneBook.class);
                    break;
                case 8:
                    return;

            }
        }

    }
}