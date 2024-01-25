<?php
class Account implements arrayaccess
{
    public string $Login;
    public string $Password;
    public string $Uid;

    public function __construct(string $login, string $password)
    {
        $this->Login = $login;
        $this->Password = $password;
        $this->Uid = $this->GenerateUid();
    }

    private function GenerateUid(): string
    {
        $data = random_bytes(16);
        $data[6] = chr(ord($data[6]) & 0x0f | 0x40); // устанавливаем версию 4
        $data[8] = chr(ord($data[8]) & 0x3f | 0x80); // устанавливаем вариант
        return vsprintf('%s%s-%s-%s-%s-%s%s%s', str_split(bin2hex($data), 4));
    }

    public function EqualTo($object): bool
    {
        if ($object instanceof Account)
            return $this->Login == $object->Login &&
                $this->Password == $object->Password;
        elseif (is_string($object))
            return $this->Login == $object;
        else return false;
    }

    public function offsetExists(mixed $offset): bool {
        return isset($this->$offset);
    }

    public function offsetGet(mixed $offset): mixed {
        return $this->$offset;
    }

    public function offsetSet(mixed $offset, mixed $value): void {
        $this->$offset = $value;
    }

    public function offsetUnset(mixed $offset): void {
        unset($this->$offset);
    }
}