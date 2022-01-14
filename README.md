## **Telematics**

Sajt je predvidjen za pracenje dostava. Moguce je pokrenuti dostavu tako sto se popune odgovarajuci podaci u gornjem levom delu sajta i klikom na dugme **Start delivery**. Nakon klika pocece simulacija dostave koja moze, zajedno sa prethodnim dostavama, da se prikaze odabirom tipa dostave i godine koje je ta dostava pokrenuta.

Nakon sto se prikazu dostave odredjenog tipa i iz zadate godine, postoji mogucnost klika na bilo koju od njih. Nakon klika ce se prikazati dodatni podaci kao sto su: **Fuel**, **Idling time**, **Speed** i **Location**, koji se beleze na svake 5 sekunde i simuliraju prikupljanje podataka sa senzora u vozilu.

Partition key za dostave je Cargo i year. Cargo je odabran zato sto je cargo parametar po kojem bi se najcesce vrsila pretraga, kao i godina u kojoj je dostava izrvrsena. Godina takodje ogranicava rast particije.

Clustering key za dostave je departing time i delivery id. Particija je sortirana po departing time-u a delivery id cini kljuc jedinstevnim.

Za **Fuel**, **Idling time**, **Speed** i **Location** partition key je delivery id, a clusterin key je reading time.

Aplikacija je predvidjena koriscenju za arhiviranje podataka i zbog toga nema mogucnost brisanja podataka.

**Upozorenje**
Baza je u cloudu i postoji mogucnost da ce da se pauzira posto je baza besplatna. Ukoliko projekat ne moze da se konektuje sa bazom posaljite nam poruku na teamsu na profilu Aleksandar Filipovic (aleksandarfilipovci) 17508
