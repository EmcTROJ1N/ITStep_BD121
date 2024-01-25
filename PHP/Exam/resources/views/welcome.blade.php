@extends("layouts/app")

@section("header")
    <div class="text-container">
        @yield("header")
        <h1>Php exam on Laravel</h1>
        <p class="p-large">Task planning (like JIRA)</p>
        @guest()
            <a class="btn-solid-lg page-scroll" href="{{ route("register") }}">SIGN UP</a>
        @endguest
        @auth()
            <form method="POST" action="{{ route('logout') }}">
                @csrf
                <a class="btn-outline-sm" href="{{ route("logout")  }}"
                   onclick="event.preventDefault();
                                                this.closest('form').submit();">
                    <a class="btn-solid-lg page-scroll" href="{{ route("logout") }}">Logout</a>
                </a>
            </form>
        @endauth
    </div> <!-- end of text-container -->
@endsection

@section("content")
@endsection

@section("footer")
@endsection
