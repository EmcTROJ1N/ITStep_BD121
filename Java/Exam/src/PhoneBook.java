import java.util.*;
public class PhoneBook {

    public List<Contact> Contacts = new ArrayList<>();

    public PhoneBook() {}

    public void ShowContacts() {
        Contacts.forEach(contact ->
                System.out.printf("%s %s %s %s %s %n", contact.FullName, contact.Phone, contact.Address, contact.Phone, contact.Description));

    }

    public boolean SaveToFile() {
        return true;
    }

    public boolean LoadFromFile() {
        return true;
    }

    public Contact AddContact(Contact contact) {
        Contacts.add(contact);
        return contact;
    }

    public boolean EditContact(int id, Contact newContact) {
        return true;
    }

    public boolean DeleteContact(int id) {
        int length = Contacts.size();
        Contacts = Contacts.stream().filter(contact -> contact.id != id).toList();

        return length != Contacts.size();
    }

    public Contact[] Search(String keyword) {
        return null;
    }
}
