# AEII_2020_BD2_ZAWIERUCHA_AQUAPARK
Projekt na bazy danych 2 - System wspomagający pracę aquaparku


## Polecenie:
![Screen](documentation/Polecenie.jpg)


## Diagram klas:

![Screen](documentation/Classes.png)

## Model bazy danych:

![Screen](documentation/DatabaseModel.png)

## Diagram przypadków użycia:

![Screen](documentation/UseCase.jpg)

## Przykładowe ekrany aplikacji:

![Screen](documentation/Landing.png)
![Screen](documentation/Login.png)
![Screen](documentation/Wristbands.png)



## Instrukcja Użytkowania

Aby korzystać z funkcji strony należy się zalogować do odpowiedniego konta.
Istnieją 4 rodzaje kont, każdy z własnym zestawem uprawnień:

#### Employee:
Login: employee@gmail.com
Hasło: ASDqwe`12

Uprawnienia:
- Przeglądanie aktualnego cennika
- Wyświetlać informacje o danym bilecie w cenniku
- Wyświetlanie informacji o opaskach i biletach na nich
- Sprzedawanie nowego biletu
- Realizacja płatności za bilety
- Zwrócenie opaski

#### Manager:
Login: manager@gmail.com
Hasło: ASDqwe`12

Uprawnienia:
- Wszystkie uprawniania Employee
- Wyświetlanie informacji o wszystkich aktywnych biletach klientów
- Wyświetlanie informacji o bramkach
- Wyświetlanie informacji o przejściach klientów przez bramki
- Wyświetlanie raportów

#### SuperManager:
Login: supermanager@gmail.com
Hasło: ASDqwe`12

Uprawnienia:
- Wszystkie uprawniania Manager
- Edycja/deaktywacja opasek
- Dodawanie nowych opasek
- Edycja/zamykanie atrakcji
- Dodawanie nowych atrakcji
- Zmiana biletów w cenniku
- Symulacja przejścia klienta przez bramkę
- Edycja/deaktywacja bramek
- Dodawanie  nowych bramek


#### Admin:
Login: admin@gmail.com
Hasło: ASDqwe`12

Uprawnienia:
- Pełen CRUD
- Wszystkie uprawniania SuperManager
- Zarządzanie użytkownikami strony
- "Ręczne" dodawanie/edycja pozycji do bazy danych