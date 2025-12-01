vcl 4.0;

backend default {
    .host = "host.docker.internal";  # special DNS name to reach Windows host from Docker
    .port = "8081";                  # your backend server port
}

sub vcl_backend_response {
    set beresp.do_esi = true;        # enable ESI parsing
}