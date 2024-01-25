<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Task extends Model
{
    use HasFactory;
    protected $fillable = ["title", "status", "user_id"];

    public function permissions(): BelongsToMany
    {
        return $this->belongsToMany(Permission::class);
    }

}
