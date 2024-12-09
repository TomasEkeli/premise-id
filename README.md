# premise-id
makes (somewhat) human-readable guids from strings

HORRIBLE IDEA, DO NOT USE FOR ANYTHING SERIOUS

## how it works

The premise-id is a string that is generated from a string by converting a short string
into something that looks like a uuid/guid. The conversion is done by converting each
character in the input string to a string of characters that are in the conversion table.

The result is not universally or globally unique. This is not a good idea. It is a bad idea.
Do not use this for anything serious.

However, if you're sick of guids you can encode small jokes in these.

## example

```bash
./prid i-hate-guids
# Encodes to
# i-hate-guids - 14a7e661-1d50-0000-0000-000000000000

./prid guids_are_not_human_readable
# Encodes to
# guids_are_not_human_readable - 6611d5a1-2e17-0746-1a4a-1712eadab2e0

# decode back to the original string (ish, sometimes, often fails)
./prid -d 6611d5a1-2e17-0746-1a4a-1712eadab2e0
# Decodes to
# 6611d5a1-2e17-0746-1a4a-1712eadab2e0 - guidsarenothumanreadable
```

## conversion table

(see `source/prid.lib/prid.cs` for the actual table in code)

| char | value |
|------|-------|
| '0'  | "0"   |
| '1'  | "1"   |
| '2'  | "2"   |
| '3'  | "3"   |
| '4'  | "4"   |
| '5'  | "5"   |
| '6'  | "6"   |
| '7'  | "7"   |
| '8'  | "8"   |
| '9'  | "9"   |
| 'a'  | "a"   |
| 'b'  | "b"   |
| 'c'  | "c"   |
| 'd'  | "d"   |
| 'e'  | "e"   |
| 'f'  | "f"   |
| 'g'  | "6"   |
| 'h'  | "4"   |
| 'i'  | "1"   |
| 'j'  | "3"   |
| 'k'  | "1c"  |
| 'l'  | "2"   |
| 'm'  | "a4"  |
| 'n'  | "17"  |
| 'o'  | "0"   |
| 'p'  | "9"   |
| 'r'  | "12"  |
| 's'  | "5"   |
| 't'  | "7"   |
| 'u'  | "61"  |
| 'v'  | "aa"  |
| 'w'  | "aaa" |
| 'x'  | "8"   |
| 'y'  | "14"  |

whitespace and punctuation is removed

## how to build

```bash
dotnet build
```

## how to run

```bash
dotnet run
```

## how to build something that can run (publish)

```bash
dotnet publish -c Release -o out

# run the published app
./out/prid the-thing-you-want-to-encode
# Encodes to
# the-thing-you-want-to-encode - 74e74117-6140-61aa-aa17-770e17c0de00
#                                |||||||´ ||´| |´|´´´||´ |||||´||||
#                                thethin  gy o u w   an  ttoen code
```

PS: please don't use this for anything serious