## UMenu Custom Control

UMenu displays a main menu object (Menu).

## Data Binding

The menu is based on view model data binding (collection of SELMenuItemVM). Each menu item is identified by MenuCommandEnum. Each submenu is populated dynamically when it is first opened. The SELMenuBinder setup an event handler for the menu opening event to retrieve the child menu items. The MNRepo is a repository of the menu items.

## ItemContainer, ItemDataTemplate, ItemStyle selectors

The Menu object can contain a list of child items of type MenuItem or Separator. The view model SELMenuItemVM can represent a MenuItem or a Separator (MenuTypeEnum). The custom selectors are required to assist data binding to select proper container, template, and style (either MenuItem or Separator) for each view model.

## Todo

Setup a command object for each menu item.