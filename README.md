﻿# AEII_2020_BD2_ZAWIERUCHA_AQUAPARK
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

Aby zrealizować płatność za bilet należy:
- w zakładce "Wristbands" wybrać opcję "Get Tickets" przy właściwej opasce
![Screen](documentation/get_ticket.png)
- wybrać "Pay Tickets", a następnie "Finish Payment"
![Screen](documentation/pay_tickets.png)
![Screen](documentation/finish_payment.png)

Aby zarejestrować zwrócenie opaski należy:
- w zakładce "Wristbands" wybrać opcję "Return Wristband" przy właściwej opasce
![Screen](documentation/return_wristband.png)

Aby sprzedać nowy bilet należy:
- w zakładce "Wristbands" wybrać opcję "Create Ticket" przy właściwej opasce
![Screen](documentation/create_ticket.png)
- wybrać rodzaj biletu, a następnie opcję "Create"
![Screen](documentation/add_ticket.png)

#### Manager:
Login: manager@gmail.com
Hasło: ASDqwe`12

Uprawnienia:
- Wszystkie uprawniania Employee
- Wyświetlanie informacji o wszystkich aktywnych biletach klientów
- Wyświetlanie informacji o bramkach
- Wyświetlanie informacji o przejściach klientów przez bramki
- Wyświetlanie raportów

Aby zobaczyć raport należy:
- w zakładce "Reports" wybrać odpowiedni wykres
![Screen](documentation/report.png)
- dodatkowo wybrać opcję "Create Report PDF", aby wygenerować plik PDF

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

Aby edytować opaskę należy:
- w zakładce "wristbands" wybrać opcję "Edit" przy odpowiedniej opasce
![Screen](documentation/supermanager_wristband.png)
- dokonać zmian i wybrać opcję "Save"
![Screen](documentation/edit_wristband.png)

Aby utworzyć opaskę należy:
- w zakładce "Wristbands" wybrać opcję "Create New"
- wypełnić wymagane pola i wybrać opcję "Create"
![Screen](documentation/create_wristband.png)

Aby edytować atrakcję należy:
- w zakładce "Attractions" wybrać opcję "Edit" przy odpowiedniej atrakcji
![Screen](documentation/supermanager_attractions.png)
- dokonać zmian i wybrać opcję "Save"
![Screen](documentation/edit_attraction.png)

Aby edytować cennik należy:
- w zakładce "PriceList" wybrać opcję "Edit" przy odpowiedniej pozycji
![Screen](documentation/supermanager_pricelist.png)
- dokonać zmian i wybrać opcję "Save"
![Screen](documentation/edit_pricelist.png)

Aby utworzyć nową pozycję w cenniku należy:
- w zakładce "PriceList" wybrać opcję "Create New"
- wypełnić wymagane pola i wybrać opcję "Create"
![Screen](documentation/create_pricelist.png)

Aby zasymulować przejście klienta przez bramkę należy:
- w zakładce "ClientEntries" wybrać opcję "Simulation"
![Screen](documentation/supermanager_cliententries.png)
- wypełnić wymagane pola i wybrać opcję "Create"
![Screen](documentation/create_entry.png)

Aby edytować bramkę należy:
- w zakładce "EntryGates" wybrać opcję "Edit" przy aktywnej bramce
![Screen](documentation/supermanager_entrygates.png)
- dokonać zmian i wybrać opcję "Save"
![Screen](documentation/edit_entrygate.png)

Aby dodać nową bramkę należy:
- w zakładce "EntryGates" wybrać opcję "Create New"
- wypełnić wymagane pola i wybrać opcję "Create"
![Screen](documentation/create_entrygate.png)

#### Admin:
Login: admin@gmail.com
Hasło: ASDqwe`12

Uprawnienia:
- Pełen CRUD
- Wszystkie uprawnienia SuperManager
- Zarządzanie użytkownikami strony
- "Ręczne" dodawanie/edycja pozycji do bazy danych

Aby trwale usunąć lub edytować dowolną pozycję należy:
- zgodnie z powyższymi instrukcjami udać się do danej zakładni i wybrać odpowiednią opcję.

Aby zarządzać użytkownikami na stronie należy:
- w zakładce "AspNetUsers" przewinąć stronę na prawo i wybrać odpowiednią z dostępnych opcji
![Screen](documentation/admin.png)