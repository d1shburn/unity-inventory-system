# Unity Inventory System

A focused Unity inventory implementation with stackable and non-stackable items.

## Features

- Atomic addition: all requested items are added or no state changes.
- Existing stacks are filled before empty cells are used.
- Non-stackable items occupy one cell each.
- Removal returns the actual number of removed items.
- UI stays synchronized through model events.
- Item definitions are configured as `ScriptableObject` assets.
