import { PackageDoc } from ".";
import { PackageDocumentationStatus, PackageDocumentationRequest } from "../reducers";

export { PackageDocumentationStatus, PackageDocumentationRequest, PackageLogState } from "../reducers";

export interface PackageContext {
    /** The package key used to request the documentation */
    pkgRequestKey: PackageKey;

    /** The package request (including request log) */
    pkgRequestStatus: PackageDocumentationRequest;

    /** The package documentation metadata (uri, log uri, etc) */
    pkgStatus: PackageDocumentationStatus;

    /** The actual package documentation */
    pkg: PackageDoc;
}