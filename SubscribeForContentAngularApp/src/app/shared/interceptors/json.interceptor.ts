import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

/**
 * Interceptor which instructs Angular to not parse the JSON it receives and instead uses its own JSON parser.
 * Used to revive dates, which sounds a lot more dubious than it actually is.
 */
@Injectable()
export class JsonParserHttpInterceptor implements HttpInterceptor {
  constructor(private readonly jsonParser: JsonParser) {}

  intercept(
    httpRequest: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return httpRequest.responseType === 'json'
      ? this.handleJsonResponses(httpRequest, next)
      : next.handle(httpRequest);
  }

  private handleJsonResponses(
    httpRequest: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next
      .handle(httpRequest.clone({ responseType: 'text' })) // Tells Angular to not process this request as JSON.
      .pipe(map((event) => this.parseResponse(event))); // Use the custom parser instead.
  }

  private parseResponse(event: HttpEvent<any>): HttpEvent<any> {
    return event instanceof HttpResponse
      ? event.clone({ body: this.jsonParser.parse(event.body) })
      : event;
  }
}

/** Custom JSON parser. */
@Injectable()
export abstract class JsonParser {
  abstract parse<T = any>(json: string): T;
}

/** JSON parser that can revive dates. */
@Injectable()
export class JsonDateParser implements JsonParser {
  parse<T = any>(json: string): T {
    return JSON.parse(json, jsonDateReviver);
  }
}

// Matching any ISO8601 string with timezone notation. Milliseconds optional. See tests for examples.
const dateStringRegex =
  /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:.\d+)?(?:Z|(\+|-)([\d|:]*))?$/;

/** Returns a date object for any ISO8601 string with timezone notation. Milliseconds optional. */
export function jsonDateReviver(_key: string, value: string): any {
  if (typeof value === 'string' && dateStringRegex.test(value)) {
    return new Date(value);
  }

  return value;
}
