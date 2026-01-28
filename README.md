## AAMVA: Brown Bag Lunch - C# Latest Features and Tips + Tricks

### Introduction
- Today we're looking at C# 11, 12, 13, and 14 features that can improve our daily code

| .NET Version | C# Version | Release Date | Retirement Date
|--------------|------------|--------------|----------------|
| .NET 10.x | C# 14 | November 2025 | Nov 2028
| .NET 9.x | C# 13 | November 2024 | Nov 2026
| .NET 8.x | C# 12 | November 2023 | Nov 2026
| .NET 7.x | C# 11 | November 2022 | May 2024

---

## C# 11 Demos

### Demo 1:  Raw String Literals
**Key Points:**
- Eliminates escape character nightmares
- `"""` syntax for multi-line strings
- `$$` for interpolation (double braces `{{ }}` for variables)
- '$$$' if double bracers are needed as literals
- '""""' if triple double quotes are needed as literals


**Notes:** 
Embedded SQL, JSON, XML, YML are bad practices in general. However, there are appropriate uses:

 - Unit Test Data + Assertions 
 - Logging + Diagnostics
 - Debugging + Prototyping
 - Code Generation
 
### Demo 2:  Required Members

**Key Points:**
- Compile-time enforcement of property initialization
- Great for DTOs and API contracts
- Works with `init` properties and records
- Helps avoid null reference issues
- Improves code readability and maintainability
- Encourages better design practices
- Reduces boilerplate code for constructors

**Notes:**
- Allowed on properties, fields, classes, structs, and records
- Not allowed on methods, indexers, events, static members, or ref members

### Demo 3: File-local Types  

**Key Points:**
- Types (classes, structs, enums, delegates, interfaces) can be declared as file-local using the `file` modifier
- Limits the scope of the type to the file it's declared in
- Enhances encapsulation and modularity
- Reduces the risk of accidental usage outside the intended context
- Improves code organization by keeping related types together
- Facilitates cleaner APIs by hiding internal types

**Notes:**
- Use cases for file-local types:
	- Code generators creating helper types
	- Implementation details that shouldn't leak
	- Avoiding naming conflicts in large projects
	- Test-specific mock implementations 
- Different than "internal" so that other classes within the assembly can't access them
- Different than "private" as private types are not allowed at the namespace level

### Demo 4: Interface Default Implementations

**Key Points:**
- Allows interfaces to provide default implementations for methods, properties, and events
- Enables adding new members to interfaces without breaking existing implementations
- Facilitates code reuse and reduces boilerplate in implementing classes

**Notes:**
- Default implementations can call other interface members
- Implementing classes can override default implementations if needed
- Useful for evolving APIs and libraries

**Caveats:**
- No instance fields / state storage
- No auto-properties with backing fields
- Default implementations cannot be called from concrete classes unless implemented explicitly
- Shouldn't be overused; interfaces should primarily define contracts
- Abstract Classes are the preferred way to have default implementations
	- Interface default implementations are more for evolving interfaces without breaking changes
	- Interface default implementations can be useful for multiple inheritance scenarios
- Unit Test Issues
	- Mocking frameworks may struggle with default interface methods
	- Test runners may have issues discovering tests in interfaces with default implementations


### Demo 5: Interface Static Abstract Members 

**Key Points:**
- Allows interfaces to declare static abstract members
- Facilitates defining contracts for static members in implementing types
- Enables more flexible and reusable code patterns
- Useful for factory patterns, mathematical operations, and more
- Compile-time polymorphism for static members
- Shared constants across implementations
- Better API design, more expressive interfaces

**Notes:**
- Static abstract members can include methods, properties, and operators
- Implementing types must provide concrete implementations for the static abstract members

---

## C# 12 Demos

### Demo 6: Primary Class Constructors

**Key Points:**
- Eliminates constructor boilerplate for DI
- Parameters available throughout the class

**Notes:**
- Parameters are mutable unless captured in readonly field

### Demo 7: Collection Expressions

**Key Points:**
- Collection intialization has constantly evolved throughout C#
- Each version has reduced boilerplate and improved readability
- Collection expressions use unified `[ ]` syntax for arrays, lists, spans, etc.

**Notes:**
- `..` spread operator to combine collections
- Natural support for interfaces and abstract classes
- Empty collection literal `[]` is now valid
- Better performance due to reduced allocations
- More concise and readable code
- Consistent pattern matching support

### Demo 8: Type Aliases

**Key Points:**
- `using` can alias any type now, including tuples
- Great for domain clarity (StateCode vs string)
- Cleans up complex generic types
- Improves code readability and maintainability
- Encourages better design practices
- Reduces boilerplate code for complex types

**Notes:**
- Useful for simplifying long generic type names
- Helps in creating more meaningful type names for better understanding

### Demo 9: List Patterns

**Key Points:**
- New pattern matching for lists and arrays
- Supports matching elements, lengths, and sub-patterns
- Enhances readability and expressiveness of pattern matching
- Useful for validating input data structures
- Enables more concise and maintainable code

**Notes:**
- Can be combined with other patterns (e.g., property patterns, tuple patterns)
- Supports nested patterns for complex data structures
- Works with any type that implements `IList<T>` or has a suitable indexer


---

## C# 13 Demos

### Demo 10: Params Collections  

**Key Points:**
- Allows `params` parameters to accept collection types like `List<T>`, `Span<T>`, etc.
- No longer limited to arrays
- Improves performance by reducing allocations, when using spans
- Enhances code readability and maintainability

**Notes:**
- Works with any type that implements `IEnumerable<T>`

### Demo 11: Implicit Indexers

**Key Points:**
- Enable using he ^int syntax for indexers in collection instanciations
- Simplifies access to elements from the end of collections
- Adds the one location where you couldn't previously use the ^ operator

**Notes:**
- Still throws exceptions for out-of-bounds access
- Niche use case, but improves consistency in syntax

### Demo 12: Partial Properties 

**Key Points:**
- One class must have the declaring part (signature, auto-implement)
- One class must have the implementing part (explicity accessor bodies)

**Notes:**
- Useful for code generators, design files, etc
	- Allows you to regenerate the class file without losing custom get/set operations
- Also useful for property change notification / triggers

---

## C# 14 Demos

### Demo 13: Extensions Everything

**Key Points:**
- Extension everything: static and instance methods, properties, operators
- Extension indexers, events, and constructors come later
- Enhances code organization and modularity

**Notes:**
- Useful for adding functionality to existing types without modifying them
- Improves code readability and maintainability

### Demo 14:  Null Conditional Assignment

**Key Points:**
- Simplifies null checks during assignments
- Prevents null reference exceptions automatically
- Improves code readability and maintainability

**Notes:**
- Finally!
