total = 0
with open("dummy.txt", "r") as fobj:
    for line in fobj.readlines():
        trimmed = line.split(":")[1].strip().split("|")
        success = set([int(i) for i in trimmed[0].split(" ") if i != ""])
        actual = set([int(i) for i in trimmed[1].split(" ") if i != ""])
        intersection = actual & success
        points = 0
        for i in intersection:
            if points == 0:
                points = 1
            else:
                points = points * 2
        total += points
print(total)
