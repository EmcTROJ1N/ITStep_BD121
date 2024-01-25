<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Http\Request;
use Symfony\Component\HttpFoundation\Response;

class CheckRole
{
    /**
     * Handle an incoming request.
     *
     * @param  \Closure(\Illuminate\Http\Request): (\Symfony\Component\HttpFoundation\Response)  $next
     */
    public function handle(Request $request, Closure $next, $role): Response
    {
        if ($request->user() && $request->user()->roles()->where('id', $role)->exists()) {
            return $next($request);
        }

        // Роль отсутствует, перенаправление или обработка ошибки
        abort(403, 'Access denied.');
    }
}
