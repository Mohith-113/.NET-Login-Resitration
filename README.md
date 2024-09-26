# .NET Login Resitration

## For Registration PROCEDURE in DB
```
CREATE PROCEDURE sp_register
    @Uname NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(15),
    @Password NVARCHAR(100),
    @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Users (Uname, Email, Phone, Password)
        VALUES (@Uname, @Email, @Phone, @Password);

        SET @ErrorMessage = 'User registered successfully';
    END TRY
    BEGIN CATCH
        SET @ErrorMessage = ERROR_MESSAGE();
    END CATCH
END

```

## For Login PROCEDURE in DB
```
CREATE PROCEDURE sp_login
    @Email NVARCHAR(100),
    @Password NVARCHAR(100),
    @ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the user exists with the given email and password
    IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email AND Password = @Password)
    BEGIN
        -- If the user exists, login is successful
        SET @ErrorMessage = 'Login Successful!';
    END
    ELSE
    BEGIN
        -- If no matching user is found, return an error message
        SET @ErrorMessage = 'Invalid Email or Password!';
    END
END;

```
