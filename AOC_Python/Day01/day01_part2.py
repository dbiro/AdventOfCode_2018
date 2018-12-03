def readInput():
    input = open("day01_input.txt", "r")
    freqs = []
    for line in input:
        freqs.append(int(line.strip()))
    return freqs

def determineDuplicatedFrequency(freqs):
    found = False
    currentFreq = 0
    computedFreqs = set()

    while not found:
        for f in freqs:
            currentFreq += f            
            if not currentFreq in computedFreqs:
                computedFreqs.add(currentFreq)
            else:
                found = True
                break
    
    return currentFreq

freqs = readInput()
duplicatedFreq = determineDuplicatedFrequency(freqs)
print(duplicatedFreq)