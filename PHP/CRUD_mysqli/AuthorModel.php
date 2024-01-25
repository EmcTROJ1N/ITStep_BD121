<?php

class AuthorDTO
{
    public $au_id;
    public $au_lname;
    public $au_fname;
    public $phone;
    public $address;
    public $city;
    public $state;
    public $zip;
    public $contract;

    public function __construct($au_id, $au_lname, $au_fname, $phone, $address, $city, $state, $zip, $contract)
    {
        $this->au_id = $au_id;
        $this->au_lname = $au_lname;
        $this->au_fname = $au_fname;
        $this->phone = $phone;
        $this->address = $address;
        $this->city = $city;
        $this->state = $state;
        $this->zip = $zip;
        $this->contract = $contract;
    }
}
