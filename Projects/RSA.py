print("Enter p&q:");
p = int(input());
q = int(input());
n = p *q;
fi = (p-1) * (q-1);
print("fi(n) = " + str(fi) + "\nn = " + str(n));
k = 0;
print("Enter e:");
e = int(input());
while ((k*fi + 1)%e) > 0:
    k = k+1;
d = int((k*fi + 1) / e);
print("d = " + str(d));
print("Input vord to crypt it:");
word = input();
wrd = [ord(char) for char in word];
print(list(wrd));
print("(e,n): " + str(e)+"/" + str(n));
print("(d,n): " + str(d)+"/" + str(n));
print("I'm crypting...");
crypted = [int(((x**e)%n)) for x in wrd];
print ("result: " + str(crypted));
print("I'm decrypting...");
print ("result: ");
decrypted = [x**d%n for x in crypted];
print(str(decrypted));
ppp = input();

