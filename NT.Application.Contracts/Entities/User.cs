namespace NT.Application.Contracts.Entities;
public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    private string password;

    public User(Guid id, string name, string email, string password)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty.", nameof(id));
        }

        ValidateName(name);
        ValidateEmail(email);
        ValidatePassword(password);

        Id = id;
        Name = name;
        Email = email;
        this.password = password;
    }

    public void ChangePassword(string newPassword)
    {
        ValidatePassword(newPassword);
        password = newPassword;
        // Possibly add logic to hash the password here
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        }
        // Add additional name validation as needed
    }

    private void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be empty.", nameof(email));
        }
        // Add additional email validation as needed
    }

    private void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password cannot be empty.", nameof(password));
        }
        // Add additional password validation as needed
    }
}
