class Claim:
    def __init__(self, id, left, top, width, height):
        self._id = id
        self._left = left
        self._top = top
        self._width = width
        self._height = height

    def __str__(self):
        return str.format("(id: {0}, left: {1}, top: {2}, width: {3}, height: {4})", self.id, self.left, self.top, self.width, self.height)

    @property
    def id(self):
        return self._id
    @property
    def left(self):
        return self._left
    @property
    def top(self):
        return self._top
    @property
    def width(self):
        return self._width
    @property
    def height(self):
        return self._height

def read_input():
    file = open("day03_input.txt", "r")
    claims = []
    for line in file:
        line_parts = line.split(" ")

        id = int(line_parts[0][1:])

        position_parts = line_parts[2][0:len(line_parts[2])-1].split(",")
        left = int(position_parts[0])
        top = int(position_parts[1])

        dimension_parts = line_parts[3].split("x")
        width = int(dimension_parts[0])
        height = int(dimension_parts[1])

        claims.append(Claim(id, left, top, width, height))
    return tuple(claims)

claims = read_input()
for c in claims:
    print(c)