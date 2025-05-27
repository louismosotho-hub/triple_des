# Triple DES (3DES) CLI

This project is a modular implementation of the Triple DES (3DES) block cipher in C#.  
It includes:

- **DES** core logic (as a building block)
- **Triple DES** (EDE mode, 2-key and 3-key)
- **Unit tests** for both DES and Triple DES
- **A command-line interface (CLI)** for encrypting and decrypting 8-byte blocks

---

## Project Structure

```
triple_des/
│
├── Core/           # Bit/byte utilities, tables, helpers
├── DES/            # DES implementation
├── TripleDES/      # Triple DES implementation & CLI
├── Tests/      # xUnit tests for DES and Triple DES
```

---

## Usage

### Build the Project

```sh
dotnet build
```

### Run the CLI

```sh
dotnet run --project TripleDES/TripleDES.csproj
```

You will be prompted for:
- Mode (`encrypt` or `decrypt`)
- Plaintext/ciphertext (8 characters or 16 hex digits)
- Three keys (each 8 characters; leave key3 blank to reuse key1 for 2-key 3DES)

### Example

```
Triple DES Cipher CLI
---------------------
Enter mode (encrypt/decrypt): encrypt
Enter text (8 characters): ABCDEFGH
Enter key1 (8 characters): 12345678
Enter key2 (8 characters): 23456789
Enter key3 (8 characters, or leave blank to reuse key1): 
Ciphertext (hex): 8F1D2E3C4B5A6978
```

---

## Running Tests

From the `Tests` directory:

```sh
dotnet test
```

All DES and Triple DES tests will run.

---

## Notes

- **DES** is included for educational purposes. For real encryption, always use **Triple DES**.
- This implementation works on 8-byte blocks. For longer data, add padding and process in blocks.
- Keys must be exactly 8 bytes (64 bits) each.