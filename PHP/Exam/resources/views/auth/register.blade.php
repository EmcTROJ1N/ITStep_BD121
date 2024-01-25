{{--
<x-guest-layout>
    <form method="POST" action="{{ route('register') }}">
        @csrf

        <!-- Name -->
        <div>
            <x-input-label for="name" :value="__('Name')" />
            <x-text-input id="name" class="block mt-1 w-full" type="text" name="name" :value="old('name')" required autofocus autocomplete="name" />
            <x-input-error :messages="$errors->get('name')" class="mt-2" />
        </div>

        <!-- Email Address -->
        <div class="mt-4">
            <x-input-label for="email" :value="__('Email')" />
            <x-text-input id="email" class="block mt-1 w-full" type="email" name="email" :value="old('email')" required autocomplete="username" />
            <x-input-error :messages="$errors->get('email')" class="mt-2" />
        </div>

        <!-- Password -->
        <div class="mt-4">
            <x-input-label for="password" :value="__('Password')" />

            <x-text-input id="password" class="block mt-1 w-full"
                            type="password"
                            name="password"
                            required autocomplete="new-password" />

            <x-input-error :messages="$errors->get('password')" class="mt-2" />
        </div>

        <!-- Confirm Password -->
        <div class="mt-4">
            <x-input-label for="password_confirmation" :value="__('Confirm Password')" />

            <x-text-input id="password_confirmation" class="block mt-1 w-full"
                            type="password"
                            name="password_confirmation" required autocomplete="new-password" />

            <x-input-error :messages="$errors->get('password_confirmation')" class="mt-2" />
        </div>

        <div class="flex items-center justify-end mt-4">
            <a class="underline text-sm text-gray-600 dark:text-gray-400 hover:text-gray-900 dark:hover:text-gray-100 rounded-md focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 dark:focus:ring-offset-gray-800" href="{{ route('login') }}">
                {{ __('Already registered?') }}
            </a>

            <x-primary-button class="ms-4">
                {{ __('Register') }}
            </x-primary-button>
        </div>
    </form>
</x-guest-layout>
--}}

@extends("auth/form")

@section("header")
    <h1>Register</h1>
    <p>Already registred?</p>
@endsection

@section("form-body")
    <form id="form" data-toggle="validator" data-focus="false" method="POST" action="{{ route("register") }}">
        @csrf
        <div class="form-group">
            <input type="text" class="form-control-input" id="ltext" name="name" required>
            <label class="label-control" for="ltext">Name</label>
            <div class="help-block with-errors"></div>
        </div>
        <div class="form-group">
            <input type="email" class="form-control-input" id="lemail" name="email" required>
            <label class="label-control" for="lemail">Email</label>
            <div class="help-block with-errors"></div>
        </div>
        <div class="form-group">
            <input type="password" class="form-control-input" name="password" id="lpassword" required>
            <label class="label-control" for="lpassword">Password</label>
            <div class="help-block with-errors"></div>
        </div>
        <div class="form-group">
            <input type="password" class="form-control-input" name="password_confirmation" id="lpassword" required>
            <label class="label-control" for="lpassword">Confirm password</label>
            <div class="help-block with-errors"></div>
        </div>
        <div class="form-group">
            <button type="submit" class="form-control-submit-button">Sign Up</button>
        </div>
        <div class="form-message">
            <div id="lmsgSubmit" class="h3 text-center hidden"></div>
        </div>
    </form>
@endsection
