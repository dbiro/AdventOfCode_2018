def readInput():
    input = open("day02_input.txt", "r")
    ids = []
    for line in input:
        ids.append(line.strip())
    return ids

def computeChecksum(ids):
    numberOfTwos = 0
    numberOfThrees = 0

    for id in ids:
        scanResult = scanId(id)
        if (scanResult[0]):
            numberOfTwos += 1
        if (scanResult[1]):
            numberOfThrees += 1

    return numberOfTwos * numberOfThrees

def scanId(id):
    occurenceOfChars = dict()
    for c in id:
        if not c in occurenceOfChars:
            occurenceOfChars[c] = 1
        else:
            occurenceOfChars[c] += 1
    hasTwos = any(v == 2 for v in occurenceOfChars.values())
    hasThrees = any(v == 3 for v in occurenceOfChars.values())
    return (hasTwos, hasThrees)

ids = readInput()
checksum = computeChecksum(ids)
print(checksum)