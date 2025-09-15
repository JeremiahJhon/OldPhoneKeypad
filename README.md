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
OldPhonePad("33#")                 // Expected: E
OldPhonePad("227*#")               // Expected: B
OldPhonePad("4433555 555666#")     // Expected: HELLO
OldPhonePad("96667775553#")        // WORLD
OldPhonePad("8 88777444666*664#")  // Expected: TURING
OldPhonePad("#")                   // "" (empty, immediate end)
OldPhonePad("2*#")                 // "" (typed 2, then erased)
OldPhonePad("0 0 0#")              // "   " (multiple spaces)
OldPhonePad("2222#")               // A (loop back to index 0)
OldPhonePad("777*777#")            // R   (first 777 removed)
OldPhonePad("4433555*5#")          // HEJ (L removed, replaced with J)
