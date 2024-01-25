<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\DB;

class TasksController extends Controller
{
    public function index()
    {
        $id = Auth::id();
        $tasks = DB::select("select * from tasks where user_id = $id");
        return view("tasks", ["tasks" => $tasks]);
    }
}
