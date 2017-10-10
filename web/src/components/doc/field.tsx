import * as React from 'react';

import { XmldocBasic, XmldocRemarks, XmldocExamples, XmldocSeeAlso } from ".";

import { PackageDoc } from "../../util";
import { IFieldEntity } from "../../structure";
import { title, declarationLocation, declaration } from "../../fragments";

interface FieldProps {
    data: IFieldEntity;
    pkg: PackageDoc;
}

export const Field: React.StatelessComponent<FieldProps> = ({ data, pkg }) => (
    <div>
        <h1>{title(pkg, data)}</h1>

        <XmldocBasic data={data.x} pkg={pkg}/>

        <h2>Declaration</h2>
        <pre className='highlight'><span className='c'>// At {declarationLocation(pkg, data)}</span><br/>{declaration(pkg, data)}</pre>

        <XmldocRemarks data={data.x} pkg={pkg}/>
        <XmldocExamples data={data.x} pkg={pkg}/>
        <XmldocSeeAlso data={data.x} pkg={pkg}/>
    </div>
);