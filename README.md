# rollo

## Tasks

### Setup

- [x] oppsett BE
- [x] oppsett FE
- [x] dev/prod branch
- [x] azure env

### BE

- [x] socket
- [x] lagre i singleton dict
- [ ] Fjerne verdier i dict når ferdig
- [ ] Periodisk fjerne verdier (da trenger games også en timestamp så vi kan slette games som er mer enn 1 time siden de started)
- [ ] Isteden for å lagre ting backend, lagre bruker id hos host, og la host sende inn en array med spillere for å caste ut hvem som er valgt i spinner

### FE

- [ ] splash
- [ ] join component
- [ ] lobby component
- [ ] color component
- [x] client for signalR
- [x] obj for å få data ??

### Features

- [ ] Fikse spin på nytt / avslutt
- [ ] Counter for antall spillere som har blitt med
- [ ] Tweake på antall iterasjoner, færre runder for mange, flere for få
- [ ] Funk for hva som skjer når du får fargen
- [ ] Mulighet for host å velge antall som skal velges ut
- [ ] Hente spørsmål / chanllenges + eliminations game

### Opp å rulle

- [x] merge til prod
- [ ] bygg fe, last opp til expo
- [ ] fiks til review
- [ ] push til review
