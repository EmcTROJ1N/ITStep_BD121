<?php

namespace Database\Seeders;

// use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use App\Models\User;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     */
    public function run(): void
    {
        $roles = [
            [
                'id' => 1,
                'title' => 'Admin',
            ],
            [
                'id' => 2,
                'title' => 'User',
            ],
        ];
        DB::table('roles')->insert($roles);

        $users = [
            [
                'id' => 666,
                'name' => 'Admin',
                'email' => 'admin@admin.com',
                'password' => bcrypt('password'),
                'remember_token' => null,
            ],
            [
                'id' => 2,
                'name' => 'User',
                'email' => 'user@user.com',
                'password' => bcrypt('password'),
                'remember_token' => null,
            ],
        ];
        DB::table('users')->insert($users);

        User::findOrFail(666)->roles()->sync(1);
    }
}
