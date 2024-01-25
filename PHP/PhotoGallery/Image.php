<?php
class Image
{

    public function __construct(string $path, string $OwnerUid)
    {
        $this->Path = $path;
        $this->OwnerUid = $OwnerUid;
    }

    public string $Uid;
    public string $Path;
    public string $OwnerUid;
}