import hashlib


def pow_mod(chislo, e, n):
    result = chislo
    for i in range(1, e):
        result = (result * chislo) % n
    return result

def get_first_5_bytes(bytes):
    result = []
    for i in range(0, 3):
        result.append(bytes[i])
    return result

def bytes_to_int(bytes):
    result = 0

    for b in bytes:
        result = result * 256 + int(b)

    return result

def int_to_bytes(value, length):
    result = []

    for i in range(0, length):
        result.append(value >> (i * 8) & 0xff)

    result.reverse()

    return result


print("Enter p&q:");
p = 9973 #9973  #int(input()); #997
q = 9967 #9967  #int(input()); #991
n = p * q;
fi = (p-1) * (q-1);
print("fi(n) = " + str(fi) + "\nn = " + str(n));
k = 0;
e = 7 #  int(input());
while ((k*fi + 1)%e) > 0:
    k = k+1;
d = int((k*fi + 1) / e);

print("(e,n): " + str(e)+"/" + str(n));
print("(d,n): " + str(d)+"/" + str(n));


word = input("Input word to make DS:");
wrdH = get_first_5_bytes(hashlib.sha1(word.encode('utf-8')).digest())
intDig = bytes_to_int(wrdH)
sigA = pow_mod(intDig, e, n)

print("A side")
print("MSG: " + word)
print("MSG digest: " + str(wrdH))
print("INT MSG digest: " + str(intDig))
print("DS: " + str(sigA))


wordB = word
intDigB = pow_mod(sigA, d, n)
wordBH = get_first_5_bytes(hashlib.sha1(wordB.encode('utf-8')).digest())
_intDigB = bytes_to_int(wordBH)

print("\nB side")
print("MSG: " + word)
print("Decoded signature: " + str(intDigB))
print("INT MSG digest: " + str(_intDigB))

if intDigB == _intDigB:
    print('Match')
else:
    print('Not match')

ppp = input();

