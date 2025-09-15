# Old Phone Pad Decoder (C#)

This project simulates typing on an **old-school mobile phone keypad** (T9-style input).  
Each number corresponds to multiple letters, and pressing the same button multiple times cycles through those letters.

For example:
- `2` → `A`
- `22` → `B`
- `222` → `C`

Special characters:
- `*` = Backspace (removes the last character)
- `#` = End of input
- `' '` (space) = Confirms the current character and moves to the next

---

## ✨ Features
- Convert numeric keypad inputs into text.
- Supports multi-press keys (like old Nokia phones).
- Handles **backspace** (`*`), **end of input** (`#`), and **space separators**.
- Easily extensible via the `Key` class.

---

## 🖥️ Usage

### Input → Output Examples
```csharp
OldPhonePad("33#")                  // Output: E
OldPhonePad("227*#")                // Output: B
OldPhonePad("4433555 555666#")      // Output: HELLO
OldPhonePad("8 88777444666*664#")   // Output: TURING
